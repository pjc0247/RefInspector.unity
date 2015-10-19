RefInspector.unity
====

`MonoBehaviour`�� �ƴ� ������Ʈ�� ���ؼ� �ڵ����� �����͸� ������ݴϴ�.

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
������Ʈ�� ������Ƽ���� �ڵ����Τ�Ÿ�Կ� �´� �����Ͱ� ǥ�õ˴ϴ�.
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
<br>
������Ƽ�� �����ϴ� Ÿ���� ������Ʈ�� ���, �ش� ������Ʈ�� ���� �����Ͱ� ǥ�õ˴ϴ�.
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
<br>
List Ÿ���� List �����Ͱ� ǥ�õ˴ϴ�. �� List �����ʹ� Ŀ���� Ŭ������ ���� ����Ʈ �����͸� �������� �ʴ� Unity 4 ���������� �����մϴ�.
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