
namespace TestProjectUsers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var autorPage = new AutorPage();
            autorPage.tbLog.Text = "NonExistentUser";
            autorPage.tbPas.Password = "InvalidPassword";
            autorPage.Auto_Btn_Click(null, null);
        }
    }
}