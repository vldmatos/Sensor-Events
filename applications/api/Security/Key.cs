using System.Text;

namespace API.Security
{
	public static class Key
	{
		internal static byte[] TokenPrivate => Encoding.ASCII.GetBytes("423aac595267474fbd51ade7367e5ec4");
	}
}
