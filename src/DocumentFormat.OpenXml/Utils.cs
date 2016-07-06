using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace DocumentFormat.OpenXml
{
    public static class XmlConvertExtra
    {
        internal static char[] crt = new char[] {'\n', '\r', '\t'};

        public static string VerifyTOKEN(string token)
        {
            if (token == null || token.Length == 0)
            {
                return token;
            }
            if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(crt) != -1 ||
                token.IndexOf("  ", StringComparison.Ordinal) != -1)
            {
                throw new XmlException("Res.Sch_NotTokenString -> " + token);
            }
            return token;
        }
    }

    /// <summary>
    /// Provider of augmented reflection data in support of conventions.
    /// </summary>
    public abstract class AttributedModelProvider
    {
        /// <summary>
        /// Provide the list of attributes applied to the specified member.
        /// </summary>
        /// <param name="reflectedType">The reflectedType the type used to retrieve the memberInfo.</param>
        /// <param name="member">The member to supply attributes for.</param>
        /// <returns>The list of applied attributes.</returns>
        public abstract IEnumerable<Attribute> GetCustomAttributes(System.Type reflectedType, MemberInfo member);

        /// <summary>
        /// <param name="reflectedType">The reflectedType the type used to retrieve the parameterInfo.</param>
        /// <param name="parameter">The member to supply attributes for.</param>
        /// <returns>The list of applied attributes.</returns>
        /// </summary>
        public abstract IEnumerable<Attribute> GetCustomAttributes(System.Type reflectedType, ParameterInfo parameter);
    }

    internal static class DirectAttributeContext2
    {
        public static IEnumerable<Attribute> GetCustomAttributes(this Attribute attributettribute, Type reflectedType,
            MemberInfo member)
        {
            if (reflectedType == null) throw new ArgumentNullException(nameof(reflectedType));
            if (member == null) throw new ArgumentNullException(nameof(member));

            if (!(member is TypeInfo) && member.DeclaringType != reflectedType)
                return EmptyArray<Attribute>.Value;

            return member.GetCustomAttributes(false);
        }


        public static IEnumerable<Attribute> GetCustomAttributes(this Attribute attributettribute, Type reflectedType,
            ParameterInfo parameter)
        {
            if (reflectedType == null) throw new ArgumentNullException(nameof(reflectedType));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            return parameter.GetCustomAttributes(false);
        }
    }

    internal class DirectAttributeContext : AttributedModelProvider
    {

        public override IEnumerable<Attribute> GetCustomAttributes(Type reflectedType, MemberInfo member)
        {
            if (reflectedType == null) throw new ArgumentNullException(nameof(reflectedType));
            if (member == null) throw new ArgumentNullException(nameof(member));

            if (!(member is TypeInfo) && member.DeclaringType != reflectedType)
                return EmptyArray<Attribute>.Value;

            return member.GetCustomAttributes(false);
        }

        public override IEnumerable<Attribute> GetCustomAttributes(Type reflectedType, ParameterInfo parameter)
        {
            if (reflectedType == null) throw new ArgumentNullException(nameof(reflectedType));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            return parameter.GetCustomAttributes(false);
        }
    }
    internal static class Extentions
    {
        public static bool IsSubclassOf(this Type source, Type other)
        {
            return
            source.GetTypeInfo().IsSubclassOf(other);
        }
    }
    internal static class EmptyArray<T>
    {
        public static readonly T[] Value = new T[0];
    }
}
