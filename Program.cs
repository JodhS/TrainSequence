using System.Text;

namespace ReadATextFile
{
	class Program
	{
		// Default folder    
		static readonly string rootFolder = @"C:\Temp\Data\";

		static void Main(string[] args)
		{
			Console.Write("Please enter the name containing schedule sequence: ");
			string? fileName = Console.ReadLine();
			string textFile = $"{rootFolder}\\{fileName}";

			if (File.Exists(textFile))
			{
				// Read a text file line by line.  
				string[] trains = File.ReadAllLines(textFile);

				int index = 1;
				var map = trains.Where(y => y.Contains(",")).Select(x => new Train()
				{
					Station = x.Split(',')[0],
					willStop = Convert.ToBoolean(x.Split(',')[1]),
					OrderIndex = index++
				}).ToList();

				string s = CalculateSequence(map);
				Console.WriteLine(s);
			}

			Console.ReadKey();
		}

		private static string CalculateSequence(List<Train> map)
		{
			int lastindex = map.FindLastIndex(x => x.willStop);

			bool isValidated = true;

			if (map.Count < 2)
			{
				isValidated = false;

				return ""; //At least two stations are required to be specified
			}
			else if (!map.First().willStop)
			{
				isValidated = false;
				return ""; //The first stop in the stop sequence should always be a stopping station
			}
			else if (map.Count >= 2 && map.Count(x => x.willStop) < 2)
			{
				isValidated = false;
				return ""; //At least two stations are required to be stoped
			}

			var map1 = map.Take(lastindex + 1).ToList();

			if (isValidated)
			{
				if (map.Count == 2 && map.All(x => x.willStop))
				{
					return $"This train stops at {map[0].Station} and {map[0].Station} only";
				}
				else if (map.All(x => x.willStop))
				{
					return "This train stops at all stations";
				}
				else if (map.Count(x => !x.willStop) == 1)
				{
					var st = map.FirstOrDefault(s => !s.willStop);
					return $"This train stops at all stations except {st?.Station}";
				}
				else if (map.Count(x => x.willStop) == 2 && map.FindLastIndex(x => x.willStop) > 2)
				{
					var lastStation = map.Last(x => x.willStop);
					return $"This train runs express from {map[0].Station} to {lastStation.Station}";
				}
				else if (map.Count(x => x.willStop) == 3)
				{
					var lastStation = map.Last(x => x.willStop);
					var firstStationIndex = map.FindIndex(x => x.willStop);
					var secondStation = map.Skip(firstStationIndex + 1).First(x => x.willStop);

					return $"This train runs express from {map[0].Station} to {lastStation.Station}, stopping only at {secondStation.Station}";
				}
				else if (map.Count(x => x.willStop) == 4)
				{
					var lastStation = map.Last(x => x.willStop);
					var firstStationIndex = map.FindIndex(x => x.willStop);
					var secondStation = map.Skip(firstStationIndex + 1).First(x => x.willStop);
					var thirdStation = map.Skip(secondStation.OrderIndex).First(x => x.willStop);

					return $"This train runs express from {map[0].Station} to {lastStation.Station}, stopping only at {secondStation.Station} and {thirdStation.Station}";
				}
				else if (map.Count(x => x.willStop) > 4)
				{
					List<string> stringBuilder = new List<string>();

					while (map.Count > 0)
					{
						if (map.Count(x => x.willStop) > 3)
						{
							var frstSequenceLimit = map.Where(x => x.willStop).Take(3).Last();
							var d = map.Where(x => x.OrderIndex <= frstSequenceLimit.OrderIndex).ToList();
							stringBuilder.Add(CalculateSequence(d));
							map.RemoveAll(x => x.OrderIndex <= frstSequenceLimit.OrderIndex);
						}
						else
						{
							stringBuilder.Add(CalculateSequence(map));
							map.Clear();
						}
					}

					return string.Join(" then ", stringBuilder);
				}
				else
				{
					return string.Empty;
				}
			}
			else
			{
				return "";
			}
		}

		public class Train
		{
			public string? Station { get; set; }

			public bool willStop { get; set; }

			public int OrderIndex { get; set; }
		}
	}
}