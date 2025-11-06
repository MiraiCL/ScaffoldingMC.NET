using System.Text;

namespace ScaffoldingMC.Models;

public record ScaffoldingCommand
{
    public static readonly List<string> Commands = [
        "c:ping","c:protocols","c:server_port","c:player_ping","NN:player_easytier_id","c:player_profiles_list"
    ];

    public byte[] ConvertToByteArray()
    {
        return [];
    }

    public static byte[] GetPingCommand(byte[]? customBody = null)
    {
        if ((customBody?.Length ?? 0) > 25) 
            throw new ArgumentOutOfRangeException(nameof(customBody), "Max length is 25.");

        var commandType = "c:ping"u8.ToArray();
        
        var typeLength = (byte)commandType.Length;
        
        var bodyLength = (uint)(customBody?.Length ?? 0);
        var bodyLengthBytes = BitConverter.GetBytes(bodyLength);
        
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bodyLengthBytes);

        var buffer = new byte[1 + typeLength + 4 + bodyLength];
        var offset = 0;

        buffer[offset++] = typeLength;

        Buffer.BlockCopy(commandType, 0, buffer, offset, commandType.Length);
        offset += commandType.Length;

        Buffer.BlockCopy(bodyLengthBytes, 0, buffer, offset, 4);
        offset += 4;

        if (customBody is not null && customBody.Length > 0)
        {
            Buffer.BlockCopy(customBody, 0, buffer, offset, customBody.Length);
        }

        return buffer;
    }

    public static byte[] GetProtocolCommand()
    {
        var body = Encoding.UTF8.GetBytes(string.Join("\0", Commands));
        var bodyLength = (uint)body.Length;
        
        var commandType = "c:protocols"u8.ToArray();
        var typeLength = (byte)commandType.Length;
        
        var bodyLengthBytes = BitConverter.GetBytes(bodyLength);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bodyLengthBytes);

        var buffer = new byte[1 + typeLength + 4 + bodyLength];
        var offset = 0;

        buffer[offset++] = typeLength;

        Buffer.BlockCopy(commandType, 0, buffer, offset, commandType.Length);
        offset += commandType.Length;

        Buffer.BlockCopy(bodyLengthBytes, 0, buffer, offset, 4);
        offset += 4;

        if (bodyLength > 0)
        {
            Buffer.BlockCopy(body, 0, buffer, offset, body.Length);
        }

        return buffer;
    }
}