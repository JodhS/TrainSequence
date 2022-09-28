# TrainSequence

Sample Trains.txt has been checked into project. 
Multiple files can be kept in the bin folder that can be used for testing.
This has been tested on Windows.


DETAIL OF THE SUMMARY OF PROJECTS

•	Using Visual Studio 2022 and the .NET 6.0 (or higher), develop a C++ or C# console application which takes one argument, the filename of the text file containing a train stop sequence and prints to console the stopping sequence description.
•	The train stop sequence shall consist of a list of station names (in traversal order), and a Boolean flag for each station indicating whether the train is stopping at the station or running express through the station. The station name and flag are separated by a comma.

Assumptions
•	The train stop sequence will always contain at least two stations.
•	The train stop sequence will always contain at least two stopping stations.
•	The first stop in the stop sequence will always be a stopping station.
•	Ignore any express stations at the end of the stop sequence that come after the last stopping station.

Output

•	If there are two stops in the stop sequence, the train will be stopping at both stations and the application shall return “This train stops at Station1 and Station2 only” where Station1 and Station2 are the names of the two stopping stations.

This train stops at Central and Roma St only

•	If the stop sequence contains three or more stations, then a more complex description shall be built:
•	If the train is stopping at all stations within the stop sequence then the application shall return “This train stops at all stations”.

This train stops at all stations

•	If the train’s stop sequence contains only one express station before the last stopping station then the application shall return “This train stops at all stations except ExpressStation”, where ExpressStation is the name of the express station.

This train stops at all stations except South Brisbane

•	If the train’s stop sequence contains an express section and the section contains two or more express stations and no intermediate stopping stations then the application shall return “This train runs express from Station1 to Station2“, where Station1 and Station2 represent the names of the stopping stations on either side of the express section.

This train runs express from Central to South Bank

•	If the train’s stop sequence contains an express section with multiple express stations and a single intermediate stopping station then the application shall return “This train runs express from Station1 to Station2, stopping only at Station3”, where Station1 and Station2 represent the names of the stopping stations on either side of the express section and Station3 is the name of the intermediate stopping station.

This train runs express from Central to Buranda, stopping only at South Bank

•	If the train’s stop sequence contains a combination of the above express sections then it will combine the text to form a compound description.

This train runs express from Central to Buranda, stopping only at South Bank then runs express from Coorparoo to Cannon Hill
 
Examples:

Central, True Roma St, True
South Brisbane, False South Bank, True

This train stops at all stations except South Brisbane



Central, True Roma St, False
South Brisbane, False South Bank, True Park Road, False Buranda, True

This train runs express from Central to Buranda, stopping only at South Bank

