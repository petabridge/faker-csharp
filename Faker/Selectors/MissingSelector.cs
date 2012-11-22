using System;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    /// Special case which lets the caller know that we were unable to find a selector for a given type
    /// </summary>
    public class MissingSelector : ITypeSelector
    {
        #region Implementation of ITypeSelector

        public int Priority
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void BeNull(bool canBeNull = false)
        {
            throw new NotImplementedException();
        }

        public bool CanBind(PropertyInfo field)
        {
            throw new NotImplementedException();
        }

        public bool CanBind(Type type)
        {
            throw new NotImplementedException();
        }

        public void Generate(object targetObject, PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public void Generate(object targetObject)
        {
            throw new NotImplementedException();
        }

        public void Generate(ref object targetObject)
        {
            throw new NotImplementedException();
        }

        public Type TargetType
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}