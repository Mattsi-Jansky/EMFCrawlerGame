import urequests as requests
import gc

def sendGET(url, debug = False):
    r = requests.get(url)
    gc.collect()
    data = r.json()
    r.close()

    if(debug):
        print("Response from GET " + url)
        print(data)
    return data

def sendPOST(url, dataDictionary, debug = False):
    r = requests.post(url, json = dataDictionary)
    gc.collect()
    response = r.status_code
    r.close()

    if(debug):
        print("Status code from POST " + url)
        print(response)
    return response