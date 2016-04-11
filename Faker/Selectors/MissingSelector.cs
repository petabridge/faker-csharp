using System;
using System.Reflection;

namespace Faker.Selectors
{
    /// <summary>
    ///     Special case which lets the caller know that we were unable to find a selector for a given type
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

        public object Generate(ref object targetObject)
        {
            throw new NotImplementedException();
        }

        public object GenerateInstance()
        {
            throw new NotImplementedException();
        }

        public Type TargetType
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected bool Equals(MissingSelector other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MissingSelector) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(MissingSelector left, MissingSelector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MissingSelector left, MissingSelector right)
        {
            return !Equals(left, right);
        }
    }
}