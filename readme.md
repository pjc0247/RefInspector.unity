RefInspector.unity
====

`MonoBehaviour`가 아닌 오브젝트에 대해서 자동으로 에디터를 만들어줍니다.

Usage
----
```c#
public TestObj a = new TestObj();

void OnInspectorGUI()
{
  a.Inspector();
}
```

Samples
----
```c#
public class Test
{
  public string stringValue { get; set; }
  public String readOnly {
    get
    {
      return "ASDF";
    }
  }
  public String nickname { get; set; }
  public int n { get; set; }
}
```
![img](img/properties.png)<br>

```c#
public class Hello
{
  public string foo { get; set; }
  public int bar { get; set; }
}
public class Test
{
  public Hello objectTest { get; set; }

  public Test()
  {
    objectTest = new Hello();
  }
}
```
![img](img/object.png)<br>

```c#
public class Test
{
  public List<int> intAry { get; set; }
  public List<Hello> objectAry { get; set; }

  public Test()
  {
    intAry = new List<int>() { 1, 2, 3, 4 };
    objectAry = new List<Hello>()
    {
      new Hello(),new Hello()
    };
  }
}
```
![img](img/arrays.png)<br>