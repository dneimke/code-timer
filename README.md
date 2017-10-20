# code-timer
CodeTimer provides a standardized way to time the end-to-end cost, and the contribution of individual code segments within a method.

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
