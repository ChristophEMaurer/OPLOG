using System;

/// <summary>
/// This class contains a password definition.
/// A client must derive a class an specify the values
/// for the abstract function. This defines the password properties.
/// 
/// Use the sample project and modify it.
/// Compile the project and copy the resulting assembly into the plugin 
/// directory.
/// </summary>
public abstract class DatabaseConnection
{
    /// <summary>
    /// This enum contains all valid character groups.
    /// The values are a bit mask and can be combined in any way.
    /// </summary>
    enum PasswordCharacterGroupFlags
    {
        /// <summary>
        /// This bit represents a digit from 0..9
        /// </summary>
        PasswordCharacterGroupFlagDigits = 1,

        /// <summary>
        /// This bit represents a digit from a..z and A..Z 
        /// in the English 26 letter alphabet.
        /// </summary>
        PasswordCharacterGroupFlagLetters = 2,

        /// <summary>
        /// This bit represents the special characteres.
        /// </summary>
        PasswordCharacterGroupFlagLettersSpecial = 4
    }

    /// <summary>
    /// This is the definition of allowed digits for bit 
    /// <see cref="PasswordCharacterGroupFlagDigits"/>
    /// </summary>
    public const string PasswordGroupDigits = "0123456789";


    /// <summary>
    /// This is the definition of allowed digits for bit 
    /// <see cref="PasswordCharacterGroupFlagLetters"/>
    /// </summary>
    public const string PasswordGroupLetters = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// This is the definition of allowed digits for bit 
    /// <see cref="PasswordCharacterGroupFlagLettersSpecial"/>
    /// </summary>
    public const string PasswordGroupSpecial = "!§$%&/()=?{[]}#'\"";

    protected abstract string ConnectionString { get; }

    /// <summary>
    /// Return the minimal required length of the password
    /// </summary>
    protected abstract int MinimumPasswordLength { get; }

    /// <summary>
    /// Return all required characters of the password.
    /// This is a bit field. You can set any value of
    /// <see cref="PasswordCharacterGroupFlags"/>
    /// </summary>
    protected abstract int RequiredPasswordCharacters { get; }

}
