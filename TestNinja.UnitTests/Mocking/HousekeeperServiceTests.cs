using System;
using NUnit.Framework;
using Moq;
using TestNinja.Mocking;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private string _statementFileName;
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _housekeeper;

        [SetUp]
        public void SetUp()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "fileName";

            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)))
            .Returns(() => _statementFileName);


            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_ShouldGenerateStatements()
        { 
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)));
        }

        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        [Test]
        public void SendStatementEmails_HousekeepersEmailIsNullWhiteSpaceOrEmptyString_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;
            _service.SendStatementEmails(_statementDate);
            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailThisStatement()
        {

            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNullWhitespaceOrEmptyString_ShouldNotEmailThisStatement(string badStatementFileName)
        {
            _statementFileName = badStatementFileName;

            _service.SendStatementEmails(_statementDate);
            VerifyEmailNotSent();
        }

        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
            _service.SendStatementEmails(_statementDate);
            VerifyMessageBoxDisplayed();
        }

        private void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }
    }
}
