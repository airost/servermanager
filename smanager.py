import tkinter as Tk
import paramiko
from tkinter import *

def connect_to_server():
    host = ipaddresstext.get()
    username = logintext.get()
    password = passwordtext.get()
    port = 2280

    client = paramiko.client.SSHClient()
    client.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    client.connect(hostname=host, username=username, password=password, port=port)
    _stdin, _stdout,_stderr = client.exec_command("df")
    print(_stdout.read().decode())
    client.closed = None
def disconnect_from_server():
    client = paramiko.client.SSHClient()
    client.close()
def df():
     client = paramiko.client.SSHClient()
     _stdin, _stdout, _stderr = client.exec_command("df")

# Okienko glowne
window=Tk()

background_image=PhotoImage(file = "dino.png")
background_label = Label(window, image=background_image)
background_label.place(x=0, y=0, relwidth=1, relheight=1)
ipaddress=Label(window, text="IP Address", fg='red')
ipaddress.place(x=80, y=30)
ipaddresstext=Entry(window, text="IP Address", bd=5)
ipaddresstext.place(x=150, y=30)
login=Label(window, text="Login", fg='red')
login.place(x=80, y=80)
logintext=Entry(window, text="Login Widget", bd=5)
logintext.place(x=150, y=80)
password=Label(window, text="Password", fg='red')
password.place(x=300, y=80)
passwordtext=Entry(window, text="Password Widget", show='*', bd=5)
passwordtext.place(x=400, y=80)
Connect=Button(window, text="Connect", fg='blue', command=connect_to_server)
Connect.place(x=80, y=100)
Disconnect=Button(window, text="Disconnect", fg='blue', command=disconnect_from_server)
Disconnect.place(x=280, y=100)
#Przyciski zarzadzajace
disklabel=Label(window, text="Disk show", fg='red')
disklabel.place(x=600, y=80)
diskbutton=Button(window, text="Disk show", fg='blue', command=df)
diskbutton.place(x=680, y=80)
startarklabel=Label(window, text="Start ARK Server", fg='red')
startarklabel.place(x=1300, y=80)
startarkbutton=Button(window, text="Start ARK Server", fg='blue', command="start ark")
startarkbutton.place(x=1400, y=80)
stoparklabel=Label(window, text="Stop ARK Server", fg='red')
stoparklabel.place(x=1300, y=110)
stoparkbutton=Button(window, text="Stop ARK Server", fg='blue', command="stop ark")
stoparkbutton.place(x=1400, y=110)

window.title('Server Manager')
window.geometry("1920x10800")
window.mainloop()