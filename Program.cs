using System;
using System.Linq;
using System.IO;

namespace replace
{
	class Program
	{
		static int _replaces = 0;

		static void Main(string[] args)
		{
			if (!CmdLineOk(args))
				return;

			string filename = args[0];
			string strtoreplace = args[1];
			string newstr = args[2];
			string tempfilename = Path.GetTempFileName();

			Console.WriteLine("Replace in {0}: '{1}' -> '{2}'", filename, strtoreplace, newstr);

			try
			{
				using(FileStream srcfs = new FileStream(filename,  FileMode.Open, FileAccess.Read))
				using (FileStream tempfs = new FileStream(tempfilename, FileMode.Open, FileAccess.Write))
				using(StreamReader srcsr = new StreamReader(srcfs))
				using (StreamWriter tmpwr = new StreamWriter(tempfs))
				{
					string srccurrline;
					while ((srccurrline = srcsr.ReadLine()) != null)
					{
						string deststr = ReplaceSubStr(srccurrline, strtoreplace, newstr);
						tmpwr.WriteLine(deststr);
					}
				}

				Console.WriteLine();

				if (_replaces > 0)
				{
					// делаем копию оригинала
					File.Copy(sourceFileName: filename, destFileName: filename + ".bak", overwrite: true);
					// удаляем оригинал
					File.Delete(filename);
					// переносим временный файл
					File.Move(sourceFileName: tempfilename, destFileName: filename);

					Console.WriteLine("{0} replaces OK!", _replaces);
				}
				else
				{
					// удаляем временный файл
					File.Delete(tempfilename);

					Console.WriteLine("OK. No replaces.");
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("Replace failed:");
				Console.WriteLine(ex.GetMessages());
			}

			Console.ReadLine();
		}

		static string ReplaceSubStr(string srcstr, string strtoreplace, string newstr)
		{
			string deststr = srcstr.Replace(strtoreplace, newstr);

			if (deststr != srcstr)
			{
				_replaces++;
				Console.Write("!");
			}
			else
				Console.Write(".");

			return deststr;
		}

		static bool CmdLineOk(string[] args)
		{
			if (args.Count() == 3)
			{
				return true;
			}
			else
			{
				Console.WriteLine(@"Use: replace.exe ""File name.ext"" ""string to replace"" ""new string""");

				return false;
			}
		}
	}
}
