using Remita.Services.Domains.Security;
using System.Text;

namespace Remita.Services.Utility;

internal class MessageIdGenerator
{
    public static string GenerateMessageId<T>(params object[] args)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"{typeof(T)}_");

        for (int i = 0; i < args.Length; i++)
        {
            if (i == args.Length)
            {
                stringBuilder.Append($"{args[i]}");

                continue;
            }
            stringBuilder.Append($"{args[i]}_");
        }

        return SHA256Hasher.Hash(stringBuilder.ToString());

    }
}
