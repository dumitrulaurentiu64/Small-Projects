using System;
using Xunit;
using Xunit.Abstractions;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    {     
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethodMultipleTimes(){
            WriteLogDelegate log = ReturnMessage;
            //log = new WriteLogDelegate(ReturnMessage);
            log += ReturnMessage;
            log += IncrementCount;

            //var result = log.Invoke("asdf");
            var result = log("cool delegate");
            AssemblyLoadEventArgs.Equals(3, count);
        }

        string ReturnMessage_Two(string message){
            count++;
            return message;
        }

        string IncrementCount(string message){
            count++;
            return message.ToLower();
        }

        [Fact]
        public void WriteLogDelegateCanPointToMethod(){
            WriteLogDelegate log;
            //log = new WriteLogDelegate(ReturnMessage);
            log = ReturnMessage;

            //var result = log.Invoke("asdf");
            var result = log("cool delegate");

            Assert.Equal("cool delegate", result);
        }
 
        string ReturnMessage(string message){
            return message;
        }

        [Fact]
        public void StringsBehavesWeird(){
            string name = "Scott";

            ChangeName(name, "Alex");

            Assert.Equal("Scott", name);
        }
 
        private void ChangeName(string name, string name_2){
            name = name_2;
        }


        [Fact]
        public void CSharpIsPassByRef(){
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }
 
        private void GetBookSetName(ref Book book, string name){
            book = new Book(name);
        }

        [Fact]
        public void CSharpIsPassByValue(){
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");



            Assert.Equal("Book 1", book1.Name);
        }
 
        private void GetBookSetName(Book book, string name){
            book = new Book(name);
        }



        [Fact]
        public void CanSetNameFromReference(){
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");



            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(Book book, string name){
            book.Name = name;
        }
        
        [Fact]
        public void  GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");
            // arrange/setup


            // act

            //assert
            Assert.NotSame(book1,book2);
        }
        [Fact]
        public void  TwoBooksCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;
            // arrange/setup


            // act

            //assert
            Assert.Same(book1,book2);
            Assert.True(Object.ReferenceEquals(book1,book2));
        }

        Book GetBook(string name)
        {
            return new Book(name);
        }
    }
}
