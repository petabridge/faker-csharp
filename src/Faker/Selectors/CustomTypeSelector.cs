using System;

namespace Faker.Selectors
{
    public class CustomTypeSelector<T> : PrimitiveSelectorBase<T>
    {
        public CustomTypeSelector(Func<T> setter)
        {
            Setter = setter;
            Priority = SelectorPriorityConstants.CustomTypePriority;
        }

        public override T Generate()
        {
            return Setter();
        }
    }
}