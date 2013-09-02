Hermes
 
<< License >>  
 
Please see LICENSE.txt.  
 
<< About >>  
 
Hermes is a little program I wrote for myself that uses Exchange Web Services 
to display an envelope icon in the notification area that lights up when you 
have unread emails. If your employer has Outlook Web Access set up that you 
can reach from outside the company network then they may also allow EWS.

If you check your email on the web at mail.whatever.com/owa then the EWS 
endpoint, if it's set up, would likely be at 
mail.whatever.com/ews/Exchange.asmx.

This program also lets you view unread messages and optionally mark them as 
read. For anything else you'd still need to access your email the regular way.

This program saves your settings in a file, settings.xml, saved wherever 
Hermes.exe is. If you select "Remember password" it will store it as well, 
but encrypted so it isn't sitting there in plain text. It uses the .NET 
wrapper around the DPAPI, which ties the encryption key to your current 
login and machine, then squirrels it away somewhere hard to get at 
programmatically. I think. At least it's good enough for what I used it for.

Posting this online in case any of it is useful to anyone else in the future.

<< Source >> 
 
The source for Hermes is available at https://github.com/mblaine/Hermes.