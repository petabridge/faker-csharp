using System;

namespace Faker.Selectors
{
    public class CustomTypeSelector<T> : PrimitiveSelectorBase<T>
    {
        public CustomTypeSelector(Func<T> setter)
        {
            Setter = setter;
        }

        public override T Generate()
        {
            return Setter();
        }
    }
}