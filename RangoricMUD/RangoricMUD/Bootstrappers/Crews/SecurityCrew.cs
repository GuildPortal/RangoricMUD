#region License

// RangoricMUD is licensed under the Open Game License.
// The original code and assets provided in this repository are Open Game Content,
// The name RangoricMUD is product identity, and can only be used as a part of the code,
//   or in reference to this project.
// 
// More details and the full text of the license are available at:
//   https://github.com/Rangoric/RangoricMUD/wiki/Open-Game-License
// 
// RangoricMUD's home is at: https://github.com/Rangoric/RangoricMUD

#endregion

#region References

using RangoricMUD.Security;

#endregion

namespace RangoricMUD.Bootstrappers.Crews
{
    public class SecurityCrew : BaseCrew
    {
        protected override void StrapOn()
        {
            Add().OfInterface<IHashProvider>().WithImplementation<CustomHashProvider>();
            Add().OfInterface<ISignInPersistance>().WithImplementation<SignInPersistance>();
            Add().OfInterface<IEncryptionProvider>().WithImplementation<EncryptionProvider>();
        }
    }
}