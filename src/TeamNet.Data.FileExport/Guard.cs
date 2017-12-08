using System;

namespace TeamNet.Data.FileExport
{
    /// <summary>
    /// Helper class for guard statements, which allow prettier
    /// code for guard clauses.
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException">The exceotion type to throw when the assertion is false.</typeparam>
        /// <param name="assertion">if set to <c>true</c> throws the <typeparamref name="TException"/> exception.</param>
        /// <param name="message">The exception message.</param>
        /// <example>
        /// Sample usage:
        /// <code>
        /// <![CDATA[
        /// Guard.Against<ArgumentException>(string.IsNullOrEmpty(name), "Name must have a value");
        /// ]]>
        /// </code>
        /// </example>
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion == false)
                return;
            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }
    }
}
