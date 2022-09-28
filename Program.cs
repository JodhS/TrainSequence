/* This program has been created by Ranjodh Sandhu 	*
 * as part of programming activity for			*
 * QLD Railways Software and Systems Engineer role.	*/
 
namespace TrainStops
{
	class Program
	{
		/// <summary>
		/// Get the Current Directory of the Program as Default folder.    
		/// </summary>
		static readonly string? rootFolder = Environment.CurrentDirectory;
		
		/// <summary>
		/// The main method. Entry point of the program.
		/// </summary>
        	static void Main()
		{
            		Console.Write("Please enter the file name containing Train sequence: ");
			string? fileName = Console.ReadLine();

			// Append the file name to the Current Directory of program.
            		string textFile = $"{rootFolder}\\{fileName}";

			if (File.Exists(textFile))
			{
				// Read a text file line by line.  
				string[] trains = File.ReadAllLines(textFile);

				int index = 1;
				// Create list of all Stations and their stops.
				var map = trains.Where(y => y.Contains(',')).Select(x => new Train()
				{
					Station = x.Split(',')[0],
					WillStop = Convert.ToBoolean(x.Split(',')[1]),
					OrderIndex = index++
				}).ToList();

				string s = CalculateSequence(map);
				Console.WriteLine(s);
			}
			else
			{
				Console.WriteLine($"File not found - '{textFile}'. Please enter a valid file.");
			}

			Console.ReadKey();
		}

		/// <summary>
		/// method to calculate the sequences based on information in the text file.
		/// </summary>
		/// <param name="map">List of the train stations and their stops.</param>
		/// <returns>The final message after calculating the sequence.</returns>
		private static string CalculateSequence(List<Train> map)
		{
			int lastindex = map.FindLastIndex(x => x.WillStop);

			bool isValidated = true;

			if (map.Count < 2)
			{
				isValidated = false;

				return "At least two stations are required to be specified";
			}
			else if (!map.First().WillStop)
			{
				isValidated = false;
				return "The first stop in the stop sequence should always be a stopping station";

            		}
			else if (map.Count >= 2 && map.Count(x => x.WillStop) < 2)
			{
				isValidated = false;
				return "At least two stations are required to be stopped";
           	 	}

			var map1 = map.Take(lastindex + 1).ToList();

			if (isValidated)
			{
				if (map.Count == 2 && map.All(x => x.WillStop))
				{
					return $"This train stops at {map[0].Station} and {map[1].Station} only";
				}
				else if (map.All(x => x.WillStop))
				{
					return "This train stops at all stations";
				}
				else if (map.Count(x => !x.WillStop) == 1)
				{
					var st = map.FirstOrDefault(s => !s.WillStop);
					return $"This train stops at all stations except {st?.Station}";
				}
				else if (map.Count(x => x.WillStop) == 2 && map.FindLastIndex(x => x.WillStop) > 2)
				{
					var lastStation = map.Last(x => x.WillStop);
					if (map[0].OrderIndex == 1)
					{
						return $"This train runs express from {map[0].Station} to {lastStation.Station}";
					}
					else // when train runs express again.
					{
						return $"runs express from {map[0].Station} to {lastStation.Station}";
                    			}
				}
				else if (map.Count(x => x.WillStop) == 3)
				{
					var lastStation = map.Last(x => x.WillStop);
					var firstStationIndex = map.FindIndex(x => x.WillStop);
					var secondStation = map.Skip(firstStationIndex + 1).First(x => x.WillStop);

					return $"This train runs express from {map[0].Station} to {lastStation.Station}, stopping only at {secondStation.Station}";
				}
				else if (map.Count(x => x.WillStop) == 4)
				{
					var lastStation = map.Last(x => x.WillStop);
					var firstStationIndex = map.FindIndex(x => x.WillStop);
					var secondStation = map.Skip(firstStationIndex + 1).First(x => x.WillStop);
					var thirdStation = map.Skip(secondStation.OrderIndex).First(x => x.WillStop);

					return $"This train runs express from {map[0].Station} to {lastStation.Station}, stopping only at {secondStation.Station} and {thirdStation.Station}";
				}
				else if (map.Count(x => x.WillStop) > 4)
				{
					List<string> stringBuilder = new List<string>();

					while (map.Count > 0)
					{
						if (map.Count(x => x.WillStop) > 3)
						{
							var frstSequenceLimit = map.Where(x => x.WillStop).Take(3).Last();
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
					// add new line with 'then' when trains runs express again.
					return string.Join("\nthen ", stringBuilder);
				}
				else
				{
					return string.Empty;
				}
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// This class hold the properties for Train.
		/// </summary>
		public class Train
		{
			/// <summary>
			/// Gets or set the station names.
			/// </summary>
			public string? Station { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether the train stops at this station or not. 
			/// </summary>
			public bool WillStop { get; set; }

			/// <summary>
			/// Gets or sets the orderindex of the station.
			/// </summary>
			public int OrderIndex { get; set; }
		}
	}
}
