from flask import Flask, jsonify, request
import pybase64
import os
import keras
import tensorflow as tf
from tensorflow.keras import layers
from tensorflow.keras import Model
import keras.utils as image
from keras.models import Sequential
import numpy as np
import cv2
import easyocr
import datetime;

app = Flask(__name__)
@app.route('/', methods=['GET'])
def helloWorld():
   if request.method == 'GET':
      return jsonify({"PieceX tutorial": "HELLO WORLD"})
@app.route('/verify', methods=['POST'])
def verify():
   if request.method == 'POST':
      verify_status = "BAD"
      frontal = request.form.get('frontal')
      decoded_data=pybase64.b64decode((frontal))
      img_file = open('/var/www/project/imagenes/frontal.jpg', 'wb')
      img_file.write(decoded_data)
      img_file.close()

      lateral = request.form.get('lateral')
      decoded_data=pybase64.b64decode((lateral))
      img_file = open('/var/www/project/imagenes/lateral.jpg', 'wb')
      img_file.write(decoded_data)
      img_file.close()

      #INICIA COMPROBACION ESTADO GENERAL
      print("Inicia validacion de estado general")
      print(datetime.datetime.now())
      base_dir = '/var/www/project/'
      model_path = os.path.join(base_dir, 'model')
      model = tf.keras.models.Sequential()
      model = keras.models.load_model(os.path.join(model_path, 'model.keras'))
      img_height = 150
      img_width = 150
      x = range(1,47)
      images_test = []

      img = image.load_img('/var/www/project/imagenes/frontal.jpg', target_size=(img_width, img_height))
      temp = image.img_to_array(img)
      images = np.vstack([np.expand_dims(temp, axis=0)])
      classes = model.predict(images)
      if(classes[0]>0.5):
         verify_status = "GOOD"
      else:
         verify_status = "BAD"
      print("Finaliza validacion de estado general")
      print(datetime.datetime.now())
      #FINALIZA COMPROBACION ESTADO GENERAL
      #INICIA COMPROBACION PEGATINAS
      print("Inicia validacion de pegatinas")
      print(datetime.datetime.now())
      lateral = cv2.imread(r"/var/www/project/imagenes/lateral.jpg")
      lateral_small = cv2.resize(lateral, (800,480) )
      lateral_grey = cv2.cvtColor(lateral_small, cv2.COLOR_RGB2GRAY)
      lateral_cropped = lateral_grey[130:390, 350:650]
      lateral_clear = cv2.bilateralFilter(lateral_cropped, 11, 17, 17)

      reader = easyocr.Reader(['es'])
      result = reader.readtext(lateral_clear, detail = 0)

      #VAMOS A VERIFICAR CUAL ES EL ELEMENTO QUE TIENE MAS NUMEROS
      for x in result:
         verify_pegatina = x
         if(porcentaje_digitos(verify_pegatina) > 50):
            break
      print("Finaliza validacion de placas")
      print(datetime.datetime.now())
      #FINALIZA PEGATINAS
      #INICIA COMPROBACION PLACAS
      print("Inicia validacion de placas")
      print(datetime.datetime.now())
      frontal_placa = cv2.imread(r"/var/www/project/imagenes/frontal.jpg")
      frontal_small = cv2.resize(frontal_placa, (620,480) )
      frontal_grey = cv2.cvtColor(frontal_small, cv2.COLOR_RGB2GRAY)
      frontal_cropped = frontal_grey[240:480, 150:470]
      frontal_clear = cv2.bilateralFilter(frontal_cropped, 11, 17, 17)
      reader = easyocr.Reader(['es'])
      result = reader.readtext(frontal_clear, detail = 0)
      for x in result:
         verify_placa = x
         if(porcentaje_digitos(verify_placa) > 50):
            break
      print("Finaliza validacion de placas")
      print(datetime.datetime.now())
      #FINALIZA COMPROBACION PLACAS
      return jsonify({"general_status": verify_status,"verify_pegatina":verify_pegatina,"verify_placa":verify_placa})
def porcentaje_digitos(cadena):
   digitos = 0
   for caracter in cadena:
      if caracter.isdigit():
         digitos += 1
   porcentaje = digitos / len(cadena) * 100
   return porcentaje
if __name__ == "__main__":
   app.run(host='0.0.0.0')
