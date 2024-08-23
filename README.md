<p style="width: 100%; background: lightyellow; border: 1px solid black; color: black; padding: 8px; text-align: center;">
This fork modifies some calls from the original library and adds fundamental functionalities for the use of coroutines. The original repository can be accessed at <a href="https://github.com/WeaverDev/More-Effective-Godot-Coroutines">https://github.com/WeaverDev/More-Effective-Godot-Coroutines</a>.
</p>


<h1 align="center"> More Effective Godot Coroutines (C#) </h1> <br>
<p align="center">
    <img alt="GDMEC Icon" title="GDMEC" src="https://github.com/WeaverDev/More-Effective-Godot-Coroutines/assets/22682921/52ce5162-872c-4f49-b5db-ee1336cddad3" width="350">
</p>

<h4 align="center">
  Unity style coroutines, without the garbage.
</p>

<br>

## Description
This is a partial port of the [MEC Free](https://assetstore.unity.com/packages/tools/animation/more-effective-coroutines-free-54975 "MEC Free") asset for [Godot 4.x](https://godotengine.org/ "Godot") .NET (C#) published with permission under the MIT license.

MEC provides Unity-style IEnumerator coroutines with zero per-frame garbage generation.

Original source by Teal Rogers at [Trinary Software](http://trinary.tech/ "Trinary Software").

## Installation
1. Download the latest commit or the stable Release version.
2. Place the `addons` folder into your project root.
3. Build the project solution under `MSBuild > Build > Build Project`

## Key Features
This is a non-exhaustive list, check the [original documentation](http://trinary.tech/category/mec/free/ "original documentation") for a more comprehensive overview. Usage is largely unchanged, aside from changes to the naming convention.

### Yielding
MEC has the following functions to yield on.
- `Timing.WaitForOneFrame` - Yields for one frame
- `Timing.WaitForSeconds(seconds)` - Yields for the given number of seconds
- `Timing.WaitUntilDone(anotherCoroutineHandle)` - Yields until the passed coroutine has ended
```cs
IEnumerator<double> AnotherCoroutine()
{
    yield return Timing.WaitForSeconds(1.0);
}

IEnumerator<double> MyCoroutine(Node node)
{
    yield return Timing.WaitForOneFrame;
    GD.Print("One frame has passed");

    yield return Timing.WaitForSeconds(2.0);
    GD.Print("Two seconds have passed");

    yield return Timing.WaitUntilDone(node, AnotherCoroutine());
    GD.Print("Finished doing something else");

    yield return Timing.WaitUntilDone(node, sound.AsCoroutine(), anim.AsCoroutine());
    GD.Print("Finished playing sound and anim");

    yield return Timing.WaitUntilAnyDone(node, sound.AsCoroutine(), anim.AsCoroutine());
    GD.Print("Finished playing sound or anim");

    yield return Timing.WaitWhile(node, () => myVar1);
    GD.Print("myVar1 == true");

    yield return Timing.WaitUntil(node, () => myVar2);
    GD.Print("myVar2 == false");
}
```

### RunCoroutine
```cs
Timing.RunCoroutine(null, MyCoroutine(null));
```
Because coroutines do not by default live on the node that started them, they will keep going after the node is freed. By using CancelWith, coroutines can be automatically cancelled when an object they depend on is lost.
```cs
Timing.RunCoroutine(myNode, MyCoroutine(myNode));
```

### Update Segments
When starting a coroutine, you can specify the time segment in which it runs.
- `Process` - Updates just before  _Process
- `PhysicsProcess` - Updates just before _PhysicsProcess
- `DeferredProcess` - Updates at the end of the current frame

```cs
Timing.RunCoroutine(myNode, MyCoroutine(), Segment.PhysicsProcess);
```

### Tags
Coroutines may be grouped by a string tag for easy global management.
```cs
Timing.RunCoroutine(myNode, MyCoroutine(), "myTag");
Timing.RunCoroutine(myNode, MyOtherCoroutine(), "myTag");
// Kill both coroutines above
Timing.KillCoroutines("myTag");
```

## Notable Differences

If you used MEC with Unity in the past, or are using the Unity documentation for reference, there are a few things to keep in mind.

### Doubles
Godot uses `double` for its scalars, so coroutines make use of `IEnumerator<double>`.

### Naming
Names have been changed to match Godot terminology, most notably:

- `Update` -> `Process`
- `GameObject` -> `Node`
- `LateUpdate` -> `DeferredProcess`

### Execution Order
To avoid ambiguous execution order at runtime depending on the tree layout, all `Timing` instances start with a `ProcessPriority` and `ProcessPhysicsPriority` of -1. This means coroutines will update before all regular nodes by default. 

### Missing Features
A handful of features have not been ported from the original MEC, such as SlowUpdate, and using WaitUntilDone on non-coroutines. These may be ported over in the future. Pull requests welcome!

## Contribution
If you spot a bug while using this, please create an [Issue](https://github.com/WeaverDev/GDMEC/issues).


## License

[MIT License](LICENSE)

Copyright (c) 2023-present, Teal Rogers, Isar Arason (WeaverDev)
