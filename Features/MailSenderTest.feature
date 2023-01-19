Feature: Email wordt verzonden naar de gebruiker nadat hij een account heeft aangemaakt zodat hij zijn email kan valideren
Scenario: Send email successfully

Given the email address "theaterlaak123@gmail.com" and subject "test email!" and text "dit is een test email negeer deze!"

When the sendMail method is called with the given email address, subject, and text

Then a message should have been sent to the user and the method returned true