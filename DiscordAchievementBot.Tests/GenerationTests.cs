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
        private ImageGenerator generator = null;

        [TestInitialize]
        public void Before()
        {
            // make the configuration that ImageGenerator needs
            Configuration config = new Configuration()
            {
                ConnectionToken = "1234"
            };

            generator = new ImageGenerator();
        }

        [TestMethod]
        public void TestPath()
        {
            string p = Path.Combine(Path.GetTempPath(), "achievement123.png");
            // test that the path is being generated correctly
            string path = generator.GenerateImagePath(123);
            Assert.AreEqual(p, path);

            string path2 = generator.GenerateImagePath(18446744073709551615);
            p = Path.Combine(Path.GetTempPath(), "achievement18446744073709551615.png");
            Assert.AreEqual(p, path2);
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
                generator.GenerateImage("TEST", 123, AchievementType.XboxOne, 123);
                generator.GenerateImage("TEST", 123, AchievementType.Xbox360, 123);
                generator.GenerateImage("TEST", 123, AchievementType.XboxOneRare, 123);

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
                generator.GenerateImage("TEST!", 123, AchievementType.XboxOne, 123);

                // delete image id 123
                generator.DeleteImage(123);
                Assert.IsTrue(true);
            }
            catch
            {
                // fail if thrown exception
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that an image can be generated and then deleted without throwing any exceptions.
        /// Tests that the file is actually removed by attempting to delete again.
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            // first generate and delete an image
            try
            {
                // create image id 123
                generator.GenerateImage("TEST!", 123, AchievementType.XboxOne, 123);

                // delete image id 123
                generator.DeleteImage(123);

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
                generator.DeleteImage(123);

                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
