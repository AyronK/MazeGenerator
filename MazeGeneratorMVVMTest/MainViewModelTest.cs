using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeGeneratorMVVM.ViewModel;

namespace MazeGeneratorMVVMTest
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MainViewModel mvm = new MainViewModel();

            Assert.AreEqual(20, mvm.Size);
            Assert.AreEqual(400, mvm.MazeCanvasSize);

            Assert.IsNotNull(mvm.GenerateMazeCommand);
            Assert.IsNotNull(mvm.MazeGeneratorChoices);

            Assert.IsNull(mvm.ViewCells);
            Assert.IsNull(mvm.ViewSolution);
            Assert.IsNull(mvm.ViewEntrance);
            Assert.IsNull(mvm.ViewExit);
        }

        [TestMethod]
        public void GenerateMazeTest()
        {
            MainViewModel mvm = new MainViewModel();
            mvm.SelectedMazeGenerator = mvm.MazeGeneratorChoices[0];
            mvm.GenerateMazeCommand.Execute(this);

            Assert.IsNotNull(mvm.ViewCells);
            Assert.IsNotNull(mvm.ViewSolution);
            Assert.IsNotNull(mvm.ViewEntrance);
            Assert.IsNotNull(mvm.ViewExit);

            Assert.IsTrue(SolutionContainsEntrance(mvm));
            Assert.IsTrue(SolutionContainsExit(mvm));
        }

        private static bool SolutionContainsExit(MainViewModel mvm)
        {
            return mvm.ViewSolution.Any(cell => cell == mvm.ViewExit);
        }

        private static bool SolutionContainsEntrance(MainViewModel mvm)
        {
            return mvm.ViewSolution.Any(cell => cell == mvm.ViewEntrance);
        }
    }
}
