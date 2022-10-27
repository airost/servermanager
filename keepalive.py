import paramiko
import re
from asyncio import open_connection
import tkinter as Tk
from tkinter import *

class ShellHandler:
    def connect_to_server(self, host, user, psw):
        host = ipaddresstext.get()
        user = logintext.get()
        psw = passwordtext.get()
        port = 2280
        self.ssh = paramiko.client.SSHClient()
        self.ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy())
        self.ssh.connect(host, username=user, password=psw, port=port)

        channel = self.ssh.invoke_shell()
        self.stdin = channel.makefile('wb')
        self.stdout = channel.makefile('r')

    def disconnect_from_server(self):
        self.ssh.close()

    def df(self):
        _stdin, _stdout, _stderr = self.ssh.exec_command("df")
        terminal.config(text = _stdout.read().decode())
        

    @staticmethod
    def command_out(cmd, out_buf, err_buf, exit_status):
        print('command executed: {}'.format(cmd))
        print('STDOUT:')
        for line in out_buf:
            print(line, end="")
        print('end of STDOUT')
        print('STDERR:')
        for line in err_buf:
            print(line, end="")
        print('end of STDERR')
        print('finished with exit status: {}'.format(exit_status))
        print('------------------------------------')
        pass

    def execute(self, cmd):
        """
        :param cmd: the command to be executed on the remote computer
        :examples:  execute('ls')
                    execute('finger')
                    execute('cd folder_name')
        """
        cmd = cmd.strip('\n')
        self.stdin.write(cmd + '\n')
        finish = 'end of stdOUT buffer. finished with exit status'
        echo_cmd = 'echo {} $?'.format(finish)
        self.stdin.write(echo_cmd + '\n')
        shin = self.stdin
        self.stdin.flush()

        shout = []
        sherr = []
        exit_status = 0
        for line in self.stdout:
            if str(line).startswith(cmd) or str(line).startswith(echo_cmd):
                # up for now filled with shell junk from stdin
                shout = []
            elif str(line).startswith(finish):
                # our finish command ends with the exit status
                exit_status = int(str(line).rsplit(maxsplit=1)[1])
                if exit_status:
                    # stderr is combined with stdout.
                    # thus, swap sherr with shout in a case of failure.
                    sherr = shout
                    shout = []
                break
            else:
                # get rid of 'coloring and formatting' special characters
                shout.append(re.compile(r'(\x9B|\x1B\[)[0-?]*[ -/]*[@-~]').sub('', line).
                             replace('\b', '').replace('\r', ''))

        # first and last lines of shout/sherr contain a prompt
        if shout and echo_cmd in shout[-1]:
            shout.pop()
        if shout and cmd in shout[0]:
            shout.pop(0)
        if sherr and echo_cmd in sherr[-1]:
            sherr.pop()
        if sherr and cmd in sherr[0]:
            sherr.pop(0)

        self.command_out(cmd=cmd, out_buf=shout, err_buf=sherr, exit_status=exit_status)
        return shin, shout, sherr

sh = ShellHandler()


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
Connect=Button(window, text="Connect", fg='blue', command=lambda: sh.connect_to_server(ipaddresstext.get(), logintext.get(), passwordtext.get()))
Connect.place(x=80, y=100)
Disconnect=Button(window, text="Disconnect", fg='blue', command=sh.disconnect_from_server)
Disconnect.place(x=280, y=100)
#Przyciski zarzadzajace
disklabel=Label(window, text="Disk show", fg='red')
disklabel.place(x=600, y=80)
diskbutton=Button(window, text="Disk show", fg='blue', command=sh.df)
diskbutton.place(x=680, y=80)
startarklabel=Label(window, text="Start ARK Server", fg='red')
startarklabel.place(x=1300, y=80)
startarkbutton=Button(window, text="Start ARK Server", fg='blue', command="start ark")
startarkbutton.place(x=1400, y=80)
stoparklabel=Label(window, text="Stop ARK Server", fg='red')
stoparklabel.place(x=1300, y=110)
stoparkbutton=Button(window, text="Stop ARK Server", fg='blue', command="stop ark")
stoparkbutton.place(x=1400, y=110)
terminal = Label(window, width=100, height=20, bg='gray', fg='white')
terminal.place(x=80, y=150)

window.title('Server Manager')
window.geometry("1920x10800")
window.mainloop()