using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace AlgorithmIntegrationTests
{
	public class InterfaceTests
	{
		private IWebDriver _driver;
		public InterfaceTests()
		{
			_driver = new ChromeDriver();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_driver.Quit();
		}

		private static IEnumerable<string> UserNameGenerator()
		{
			for (int i = 0; i < 10; i++)
			{
				yield return $"generated-developer{i}";
			}
		}

		[TestCaseSource(nameof(UserNameGenerator)), Order(1)]
		public void AddDevelopersTests(string pseudonym)
		{
			_driver.Navigate().GoToUrl("https://localhost:7068/Developers/Create");

			_driver.FindElement(
				By.XPath(
					"//*[text()='FirstName']/following::input[1]"
				)
			).SendKeys("John");

			_driver.FindElement(
				By.XPath(
					"//*[text()='LastName']/following::input[1]"
				)
			).SendKeys("Doe");

			_driver.FindElement(
				By.XPath(
					"//*[text()='Pseudonym']/following::input[1]"
				)
			).SendKeys(pseudonym);

			_driver.FindElement(By.XPath("//*[@value='Create']")).Click();
		}

		[TestCaseSource(nameof(UserNameGenerator)), Order(100)]
		public void DeleteDevelopersTests(string pseudonym)
		{
			_driver.Navigate().GoToUrl("https://localhost:7068/Developers");

			_driver.FindElement(
				By.XPath(
					$"//td[contains(.,'{pseudonym}')]/following::td[1]/a[3]"
				)
			).Click();

			_driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
		}
	}
}
