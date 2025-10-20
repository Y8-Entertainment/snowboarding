#if UNITY_EDITOR
using NUnit.Framework;

public class MathSmokeTests
{
    [Test]
    public void OnePlusOne_EqualsTwo()
    {
        Assert.AreEqual(2, 1 + 1);
    }
}
#endif
