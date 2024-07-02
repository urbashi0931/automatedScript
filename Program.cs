using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HospitalManagementAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize WebDriver
            IWebDriver driver = new ChromeDriver();

            try
            {
                // Step 1: Access the application
                driver.Navigate().GoToUrl("https://staging-scweb.arcapps.org/");

                // Step 2: Login
                driver.FindElement(By.XPath("//input[@placeholder='Enter Your Username']")).SendKeys("tester");
                driver.FindElement(By.Name("password")).SendKeys("tester2023!");
                driver.FindElement(By.XPath("//*[@id=\"root\"]/div[2]/div[2]/div/div/div[3]/form/div/div[4]/button")).Click();

                //Step 3: option selection page

                // Click on the dropdown
                driver.FindElement(By.XPath("//option[@value='1' and text()='Lusaka']")).Click();

                // Find the option with value '1' and text 'Lusaka' and click it using JavaScript Executor
               IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
               executor.ExecuteScript("arguments[1].click();", driver.FindElement(By.XPath("//option[@value='1' and text()='Lusaka']")));



                //driver.FindElement(By.XPath("//option[@value='5' and text()='Lusaka']")).Click();
                //executor.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("//option[@value='5' and text()='Lusaka']")));
                
                
                driver.FindElement(By.Id("facility")).SendKeys("Dr Watson Dental Clinic");
                

                // Wait for navigation to complete
                Thread.Sleep(3000);

                // Step 3: Enter NRC and navigate
                driver.FindElement(By.Id("nrc")).SendKeys("111111111");
                driver.FindElement(By.Id("attendButton")).Click();

                // Wait for navigation to "Vitals"
                Thread.Sleep(3000);

                // Step 4: Add Vital
                driver.FindElement(By.Id("addVitalButton")).Click();

                // Step 5: Upload the sample dataset
                driver.FindElement(By.Id("uploadInput")).SendKeys("\"C:\\Users\\Admin\\Downloads\\Sample Dataset (1).csv"); // Update path to your dataset file

                // Step 6: Submit the upload
                driver.FindElement(By.Id("submitButton")).Click();

                // Wait for the upload to complete
                Thread.Sleep(3000);

                // Step 7: Verify upload success
                var successMessage = driver.FindElement(By.Id("successMessage"));
                if (successMessage.Displayed)
                {
                    Console.WriteLine("Test Passed: File uploaded successfully");
                }
                else
                {
                    Console.WriteLine("Test Failed: Upload failed or success message not displayed");
                }
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}
