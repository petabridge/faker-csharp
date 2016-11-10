using System;

namespace Faker.Selectors
{
    public class CustomTypeSelector<T> : PrimitiveSelectorBase<T>
    {
        public CustomTypeSelector(Func<T> setter)
        {
            Setter = setter;
            Priority = SelectorConstants.CustomTypePriority;
        }

        public override T Generate()
        {
            return Setter();
        }
    }
}