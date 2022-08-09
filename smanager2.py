# import paramiko
# import pyqt5
# def connect_to_server():
#     host = 'ipaddresstext.get()'
#     username =' logintext.get()'
#     password =' passwordtext.get()'
#     port = 2280

#     client = paramiko.client.SSHClient()
#     client.set_missing_host_key_policy(paramiko.AutoAddPolicy())
#     client.connect(hostname=host, username=username, password=password, port=port)
#     _stdin, _stdout,_stderr = client.exec_command("df")
#     print(_stdout.read().decode())
#     client.closed = None
# def disconnect_from_server():
#     client = paramiko.client.SSHClient()
#     client.close()
# def df():
#      client = paramiko.client.SSHClient()
#      _stdin, _stdout, _stderr = client.exec_command("df")

import sys

locate_python = sys.exec_prefix

print(locate_python)

