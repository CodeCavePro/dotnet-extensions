// ReSharper disable CheckNamespace

#if !NETSTANDARD1_6

namespace CodeCave.Equipple.Extensions
{
    /// <summary>
    /// Provides access to all the Environments with the following fall-back order: Process -> User -> Machine.
    /// </summary>
    public static class EnvironmentExtension
    {
        /// <summary>
        /// Tries to get an environment variable
        /// from all <see cref="EnvironmentVariableTarget"/> scopes:
        /// Process, User, Machine (falling back in this exact order).
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns>The value of the variable in
        /// if has been defined in any of these <exception cref="EnvironmentVariableTarget"></exception> scopes:
        /// Process, User, Machine.
        /// </returns>
        public static string GetEnvironmentVariable(this string variableName) => System.Environment.GetEnvironmentVariable(variableName, System.EnvironmentVariableTarget.Process)
                   ?? System.Environment.GetEnvironmentVariable(variableName, System.EnvironmentVariableTarget.User)
                   ?? System.Environment.GetEnvironmentVariable(variableName, System.EnvironmentVariableTarget.Machine);
    }
}

#endif
