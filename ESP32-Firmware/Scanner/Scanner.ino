/*
   Based on Neil Kolban example for IDF: https://github.com/nkolban/esp32-snippets/blob/master/cpp_utils/tests/BLE%20Tests/SampleScan.cpp
   Ported to Arduino ESP32 by Evandro Copercini
*/

#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEScan.h>
#include <BLEAdvertisedDevice.h>

int scanTime = 1; //In seconds
int rssi_1, rssi_2, rssi_3;
BLEScan* pBLEScan;
BLEAdvertisedDevice device;
int rssi;

class MyAdvertisedDeviceCallbacks: public BLEAdvertisedDeviceCallbacks {
    void onResult(BLEAdvertisedDevice advertisedDevice) {     
        if (advertisedDevice.getName() == "Beacon-1") {
            rssi_1 = advertisedDevice.getRSSI();
        } else if (advertisedDevice.getName() == "Beacon-2") {
            rssi_2 = advertisedDevice.getRSSI();     
        } else if (advertisedDevice.getName() == "Beacon-3") {
            rssi_3 = advertisedDevice.getRSSI();       
        }
      }
};

void setup() {
  Serial.begin(115200);
  Serial.println("Scanning...");

  BLEDevice::init("");
  pBLEScan = BLEDevice::getScan(); //create new scan
  pBLEScan->setAdvertisedDeviceCallbacks(new MyAdvertisedDeviceCallbacks());
  pBLEScan->setActiveScan(true); //active scan uses more power, but get results faster
  pBLEScan->setInterval(1000);
  pBLEScan->setWindow(99);  // less or equal setInterval value
}

void loop() {
  // put your main code here, to run repeatedly:
  BLEScanResults foundDevices = pBLEScan->start(scanTime, false);
  //Serial.print("Devices found: ");
  //Serial.println(foundDevices.getCount());
  for (int i = 0; i < foundDevices.getCount(); i++) {
    device = foundDevices.getDevice(i);
    rssi = device.getRSSI();
    //Serial.println(rssi);
  }
  //Serial.println("Scan done!");
  pBLEScan->clearResults();   // delete results fromBLEScan buffer to release memory
  delay(100);

  Serial.println("*" + String("Beacon 1 = ") + rssi_1 + "#" + String("Beacon 2 = ") + rssi_2 + "#" + String("Beacon 3 = ") + rssi_3 + "*");
}