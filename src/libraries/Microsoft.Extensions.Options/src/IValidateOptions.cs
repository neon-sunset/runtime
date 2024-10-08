// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.Extensions.Options
{
    /// <summary>
    /// Validates options.
    /// </summary>
    /// <typeparam name="TOptions">The options type to validate.</typeparam>
    public interface IValidateOptions<TOptions> where TOptions : class
    {
        /// <summary>
        /// Validates a specified named options instance (or all if <paramref name="name"/> is <see langword="null"/>).
        /// </summary>
        /// <param name="name">The name of the options instance being validated.</param>
        /// <param name="options">The options instance.</param>
        /// <returns>The <see cref="ValidateOptionsResult"/> result.</returns>
        ValidateOptionsResult Validate(string? name, TOptions options);
    }
}
