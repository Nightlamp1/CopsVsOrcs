using System;
using System.Text;

public class Base64 {
  public static string encodeString(string input) {
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
  }

  public static string decodeString(string input) {
    return Encoding.UTF8.GetString(Convert.FromBase64String(input));
  }

  public static string encodeDateTime(DateTime input) {
    return Convert.ToBase64String(BitConverter.GetBytes(input.ToBinary()));
  }

  public static DateTime decodeDateTime(string input) {
    return DateTime.FromBinary(BitConverter.ToInt64(Convert.FromBase64String(input), 0));
  }
}
