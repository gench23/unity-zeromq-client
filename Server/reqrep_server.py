import time
import random
import zmq

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:12345")

while True:
    #  wait request from client
    message_rx = socket.recv()
    print(f"Received request: {message_rx}")

    #  do something
    time.sleep(1)

    #  reply to client
    message = str(random.uniform(-1.0,1.0)) + " " + str(random.uniform(-1.0,1.0))
    print(message)
    socket.send_string(message)