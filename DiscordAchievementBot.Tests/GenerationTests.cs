using System;
using DiscordAchievementBot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;

namespace DiscordAchivementBot.Tests
{
    [TestClass]
    public class GenerationTests
    {
        private ImageGenerator m_generator = null;

        [TestInitialize]
        public void Before()
        {
            // make the configuration that ImageGenerator needs
            Configuration config = new Configuration()
            {
                ConnectionToken = "1234", ImageTemporaryDirectory = Directory.GetCurrentDirectory()
            };

            m_generator = new ImageGenerator(config);
        }

        [TestMethod]
        public void TestPath()
        {
            string p = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;

            // test that the path is being generated correctly
            string path = m_generator.GenerateImagePath(123);
            string correctPath = System.Environment.ExpandEnvironmentVariables(p);
            correctPath += "achievement123.png";
            Assert.AreEqual(path, correctPath);

            string path2 = m_generator.GenerateImagePath(18446744073709551615);
            string correctPath2 = System.Environment.ExpandEnvironmentVariables(p);
            correctPath2 += "achievement18446744073709551615.png";
            Assert.AreEqual(path2, correctPath2);
        }

        [TestMethod]
        [DeploymentItem(@"Resources\xboxone.png", @"Resources")]
        [DeploymentItem(@"Resources\xboxonerare.png", @"Resources")]
        [DeploymentItem(@"Resources\test.png", @"Resources")]
        public void GenerateTest()
        {
            try
            {
                // try generating one of each
                m_generator.GenerateImage("TEST", 123, AchievementType.XboxOne, 123);
                m_generator.GenerateImage("TEST", 123, AchievementType.Xbox360, 123);
                m_generator.GenerateImage("TEST", 123, AchievementType.XboxOneRare, 123);

                Assert.IsTrue(true);
            } catch
            {
                Assert.Fail();
            }
        }
        
        [TestMethod]
        public void GenerateAndDeleteTest()
        {
            try
            {
                // create image id 123
                m_generator.GenerateImage("TEST!", 123, AchievementType.XboxOne, 123);

                // delete image id 123
                m_generator.DeleteImage(123);

                Assert.IsTrue(true);
            }
            catch
            {
                // fail if thrown exception
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            // first generate and delete an image
            try
            {
                // create image id 123
                m_generator.GenerateImage("TEST!", 123, AchievementType.XboxOne, 123);

                // delete image id 123
                m_generator.DeleteImage(123);

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }

            // try deleting that again
            // if it throws an exception, then that means it was deleted that first time
            // and this is good
            try
            {
                // delete image id 123
                m_generator.DeleteImage(123);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
