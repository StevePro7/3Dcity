﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WindowsGame.Define.Interfaces
{
	public interface IFileManager
	{
		IList<String> LoadTxt(String file);
		T LoadXml<T>(String file);
		XElement LoadXElement(String file);
	}
}