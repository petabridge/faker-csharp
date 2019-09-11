using System;
using System.Collections.Generic;
using System.Linq;
using Faker.Helpers;
using Faker.Selectors;
using FsCheck;
using FsCheck.Experimental;
using Xunit;

namespace Faker.Models.Tests
{
    public static class TypeTableExtensions
    {

        private class TypeSelectorComparer : IEqualityComparer<ITypeSelector>
        {
            public bool Equals(ITypeSelector x, ITypeSelector y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(ITypeSelector obj)
            {
                return obj.GetHashCode();
            }
        }

        public static readonly IEqualityComparer<ITypeSelector> SelectorComparer = new TypeSelectorComparer();

        public static bool TypeTablesEqual(this TypeTable table, TypeTable other)
        {
            foreach (var type in table.TypeMap.Keys.Concat(other.TypeMap.Keys).Distinct())
            {
                var otherTypes = other.GetSelectors(type).ToList();
                var sameElements = table.GetSelectors(type).TypeSelectorListsEqual(otherTypes);
                if (!sameElements) return false;
            }
            return true;
        }

        public static bool TypeSelectorListsEqual(this IEnumerable<ITypeSelector> set1, IEnumerable<ITypeSelector> set2)
        {
            return ReferenceEquals(set1, set2) || set1.OrderBy(x => x.GetHashCode()).SequenceEqual(set2.OrderBy(x => x.GetHashCode()), SelectorComparer);
        }
    }

    
    public class TypeTableSpecs
    {
        public TypeTableSpecs()
        {
            Arb.Register<FakerGenerators>();
        }

        [Fact]
        public void IdenticalTypeSelector_list_should_be_equivalent_in_any_order()
        {
            Prop.ForAll<ITypeSelector[]>(selectors =>
            {
                var shuffled = selectors.Shuffle();
                return selectors.TypeSelectorListsEqual(shuffled).When(selectors.Length > 1 && selectors.Distinct().Count() > 1);
            }).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void TypeTable_unmodified_clones_should_be_equivalent()
        {
            Prop.ForAll<TypeTable>(
                (table) =>
                {
                    var clone = table.Clone();
                    return clone.TypeTablesEqual(table);
                }).QuickCheckThrowOnFailure();
        }


        [Fact(DisplayName = "TypeTable.Clone() should produce an immutable object")]
        public void TypeTable_clones_should_be_immutable()
        {
            Prop.ForAll<TypeTable, ITypeSelector[], ITypeSelector[]>(
                (table, originalSelectors, cloneSelectors) =>
                {
                    var clone = table.Clone();

                    // add selectors to our original
                    foreach (var selector in originalSelectors)
                    {
                        table.AddSelector(selector);
                    }

                    // add selectors to our clone
                    foreach (var selector in cloneSelectors)
                    {
                        clone.AddSelector(selector);
                    }

                    return (!clone.TypeTablesEqual(table))
                    .When(!originalSelectors.TypeSelectorListsEqual(cloneSelectors) // can't have the same list
                        && (originalSelectors.Length >= 1 || cloneSelectors.Length >= 1)); // need at least 1 selector
                }).QuickCheckThrowOnFailure();
        }
    }

    public class TypeTableState
    {
        public IDictionary<Type, LinkedList<ITypeSelector>> Selectors => Original.TypeMap;
        public TypeTable Original;
        public bool Copied;
        public int OriginalMutations;
    }

    //public class TypeTableModelExperimental : FsCheck.Experimental.Machine<TypeTable, TypeTableState> {
    //    public override Gen<Operation<TypeTable, TypeTableState>> Next(TypeTableState obj0)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Arbitrary<Setup<TypeTable, TypeTableState>> Setup
    //    {
    //        get
    //        {
    //            Arbitrary<Setup<TypeTable, TypeTableState>>
    //        }
    //    }
    //}

    public class TypeTableModel : ICommandGenerator<TypeTable, TypeTableState>
    {
        public Gen<Command<TypeTable, TypeTableState>> Next(TypeTableState obj0)
        {
            return
                Gen.Elements(new Command<TypeTable, TypeTableState>[]
                {
                    new RegisterSelectorBeforeAnyClones(_typeSelectorRandomizer),
                    new RegisterSelectorOnClone(_typeSelectorRandomizer), new CloneOriginal(),
                    new RegisterSelectorOnOriginal(_typeSelectorRandomizer),
                });
        }

        private readonly Func<ITypeSelector> _typeSelectorRandomizer =
            () =>
                (new ITypeSelector[]
                {
                    new IntSelector(), new GuidSelector(), new StringSelector(), new TimeStampSelector(),
                    new DoubleSelector(), new DateTimeSelector(), new EmailSelector()
                }).GetRandom();

        private readonly Lazy<TypeTable> _sharedInitial = new Lazy<TypeTable>(() => new TypeTable(false));
        public TypeTable InitialActual => _sharedInitial.Value;
        public TypeTableState InitialModel => new TypeTableState() { Copied = false, Original = InitialActual };

        private class RegisterSelectorBeforeAnyClones : Command<TypeTable, TypeTableState>
        {
            private readonly ITypeSelector Selector;

            public RegisterSelectorBeforeAnyClones(Func<ITypeSelector> selector)
            {
                Selector = selector();
            }

            public override TypeTable RunActual(TypeTable obj0)
            {
                obj0.AddSelector(Selector);
                return obj0;
            }

            public override TypeTableState RunModel(TypeTableState obj0)
            {
                obj0.OriginalMutations++;
                return obj0; //should be updated automatically
            }

            public override bool Pre(TypeTableState _arg1)
            {
                return _arg1.Copied == false;
            }

            public override Property Post(TypeTable _arg2, TypeTableState _arg3)
            {
                return ((ReferenceEquals(_arg2, _arg3.Original)) && (_arg2.CountSelectors(Selector.TargetType)
                    == _arg3.Original.CountSelectors(Selector.TargetType))).ToProperty()
                    .Label($"Expected actual and model to share same state for{Selector.TargetType}; Actual(Count={_arg2.CountSelectors(Selector.TargetType)}), Model(Count={_arg3.Original.CountSelectors(Selector.TargetType)}]");
            }
        }

        private class RegisterSelectorOnClone : Command<TypeTable, TypeTableState>
        {
            private int _modelCount;
            private int _actualCount;

            private readonly ITypeSelector _selector;

            public RegisterSelectorOnClone(Func<ITypeSelector> selector)
            {
                _selector = selector();
            }

            public override TypeTableState RunModel(TypeTableState obj0)
            {
                _modelCount = obj0.Original.CountSelectors(_selector.TargetType);
                return obj0;
            }

            public override TypeTable RunActual(TypeTable obj0)
            {
                _actualCount = obj0.CountSelectors(_selector.TargetType);
                obj0.AddSelector(_selector);
                return obj0;
            }

            public override bool Pre(TypeTableState _arg1)
            {
                return _arg1.Copied && _arg1.OriginalMutations > 0;
            }

            public override Property Post(TypeTable _arg2, TypeTableState _arg3)
            {
                return (_arg2.CountSelectors(_selector.TargetType) > _actualCount &&
                   _modelCount == _arg3.Selectors[_selector.TargetType].Count).ToProperty()
                   .Label($"Expected clone to increase selector count for  [{_selector.GetType()}] and original to stay same; but clone had [old: {_actualCount},new: {_arg2.CountSelectors(_selector.TargetType)}] and original had [old:, {_modelCount}, new: {_arg3.Selectors[_selector.TargetType].Count}].");
            }
        }

        private class RegisterSelectorOnOriginal : Command<TypeTable, TypeTableState>
        {
            private int _modelCount;
            private int _actualCount;

            private readonly ITypeSelector _selector;

            public RegisterSelectorOnOriginal(Func<ITypeSelector> selector)
            {
                _selector = selector();
            }

            public override TypeTableState RunModel(TypeTableState obj0)
            {
                _modelCount = obj0.Original.CountSelectors(_selector.TargetType);
                obj0.Original.AddSelector(_selector);
                obj0.OriginalMutations++;
                return obj0;
            }

            public override TypeTable RunActual(TypeTable obj0)
            {
                _actualCount = obj0.CountSelectors(_selector.TargetType);
                return obj0;
            }

            public override bool Pre(TypeTableState _arg1)
            {
                return _arg1.Copied && _arg1.OriginalMutations > 0;
            }

            public override Property Post(TypeTable _arg2, TypeTableState _arg3)
            {
                return (_arg2.CountSelectors(_selector.TargetType) == _actualCount &&
                   _modelCount > _arg3.Selectors[_selector.TargetType].Count).ToProperty()
                   .Label($"Expected original to increase selector count for  [{_selector.GetType()}] and clone to stay same; but clone had [old: {_actualCount},new: {_arg2.CountSelectors(_selector.TargetType)}] and original had [old:, {_modelCount}, new: {_arg3.Selectors[_selector.TargetType].Count}, mutations: {_arg3.OriginalMutations}].");
            }
        }

        private class CloneOriginal : Command<TypeTable, TypeTableState>
        {
            public override TypeTable RunActual(TypeTable obj0)
            {
                obj0 = obj0.Clone();
                return null;
            }

            public override TypeTableState RunModel(TypeTableState obj0)
            {
                obj0.Copied = true;
                return obj0;
            }

            public override bool Pre(TypeTableState _arg1)
            {
                return !_arg1.Copied && _arg1.OriginalMutations > 0;
            }

            public override Property Post(TypeTable _arg2, TypeTableState _arg3)
            {
                return (_arg3.Copied && !ReferenceEquals(_arg2, _arg3.Original)).ToProperty();
            }
        }
    }
}
