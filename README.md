# code-timer
CodeTimer provides a standardized way to time the end-to-end cost, and the contribution of individual code segments within a method.

**Build** [![Build status](https://ci.appveyor.com/api/projects/status/1mocaf3ycpxjpawa?svg=true)](https://ci.appveyor.com/project/dneimke/code-timer)

**NuGet** [![nuget](https://img.shields.io/nuget/v/codetimer.svg)](https://www.nuget.org/packages/codetimer/)

## Installation

```sh
Nuget
Install-Package codetimer -Version 0.0.12

dotnet CLI
dotnet add package codetimer --version 0.0.12
```

## Usage example

```csharp
public List<Blah> DoSomething() {

    var timer = new CodeTimer("DoSomething", logger);

    var data = MakeDatabaseCall();
    timer.Mark("Get initial data");

    myWebService.CallSomething(data.Id);
    timer.Mark("Updated backend service");

    var coll = new List<Blah>();
    foreach(var foo in data.Bars) {
        coll.Add(foo.Blah);
    }
    timer.Mark("Created list of Blah's");

    // Stops the timer and logs the result 
    // Logs as Error if (Success == false)
    timer.Complete(); 

    return coll;
}
```

## Formatting Example
The CodeTimer has a pluggable formatter for returning results and which is used to write to Logs if an ILogger is passed in to the constructor.

The example below displays an example result which is returned by the default Formatter.

```csharp
var codeTimer = new CodeTimer("Case1", 1000);

// Do something
codeTimer.Mark("Start");  // 400ms

// Do another thing
codeTimer.Mark("Middle"); // 800ms

// Do last thing
codeTimer.Mark("End"); // 1200ms
codeTimer.Complete(); // 1201ms

Console.WriteLine(codeTimer.GetFormattedResult())

// Displays
Case1 timer failed.  Ran for 1201ms.  Expected 1000ms
 - Start: 400ms
 - Middle: 800ms
 - End: 1200ms
```

## Meta

Darren Neimke – [@digory](https://twitter.com/digory)

Distributed under the MIT license. See ``LICENSE`` for more information.

[https://github.com/dneimke/code-timer/blob/master/LICENSE](https://github.com/dneimke/code-timer/blob/master/LICENSE)

## Contributing

1. Fork it (<https://github.com/yourname/yourproject/fork>)
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request