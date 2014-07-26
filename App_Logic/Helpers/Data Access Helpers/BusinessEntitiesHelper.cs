using System;
namespace Eisk.Helpers
{
    partial class BusinessLayerHelper
    {
        public static void ThrowErrorForInvalidDataKey(string dataFieldName)
        {
            throw new InvalidDataKeyException("Data field is not a valid data key. Data field name: " + dataFieldName);
        }

        public static void ThrowErrorForEmptyValue(string dataFieldName)
        {
            throw new EmptyValueNotAllowedException("Data field is not allowed to be empty. Data field name: " + dataFieldName);
        }

        public static void ThrowErrorForNullValue(string dataFieldName)
        {
            throw new NullValueNotAllowedException("Data field is not allowed to be null. Data field name: " + dataFieldName);
        }
    }
}
