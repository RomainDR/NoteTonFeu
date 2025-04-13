using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LoginServiceTest
    {
        [UnityTest]
        [TestCase(0, 0, 0, ExpectedResult = null)]
        [TestCase(1, 2, 3, ExpectedResult = null)]
        public IEnumerator Naaaa(int a, int b, int result)
        {
            
            Assert.AreEqual(a + b, result);

            
            var task = Test2();
            yield return new WaitUntil(() => task.IsCompleted); 
            
            int test = task.Result;
            Assert.AreEqual(2, test);
        }

        private Task<int> Test2()
        {
            return Task.FromResult(2);
        }
    }
}