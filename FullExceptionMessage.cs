using System;

namespace replace
{
	/// <summary>
	/// Расширение для получения подного сообщения об ошибке
	/// </summary>
	public static class FullExceptionMessage
	{
		/// <summary>
		/// Собирает строку из сообщений переданного и вложенных исключений
		/// </summary>
		/// <param name="e">Исключение из которого надо получить сообщение об ошибке</param>
		/// <returns>Собранные в строку сообщения из исключений</returns>
		public static string GetMessages(this Exception e)
		{
			string msgs = e.GetType() + ". " + e.Message;
			Exception ex = e;
			while (ex.InnerException != null)
			{
				msgs += ": " + ex.InnerException.Message;
				ex = ex.InnerException;
			}

			return msgs;
		}
	}
}
