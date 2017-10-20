# code-timer
CodeTimer provides a standardized way to time the end-to-end cost, and the contribution of individual code segments within a method.

[![Build status](https://ci.appveyor.com/api/projects/status/1mocaf3ycpxjpawa?svg=true)](https://ci.appveyor.com/project/dneimke/code-timer)

## Installation

```sh
npm install code-timer --save
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