import requests
import urllib.parse

from datetime import datetime, timedelta
from flask import Flask, request, jsonify, redirect, session


app = Flask(__name__)
app.secret_key = "YOUR_SECRET"


CLIENT_ID = "YOUR_CLIENT_ID"
CLIENT_SECRET = "Your_CLIENT_SECRET"
REDIRECT_URI = "http://localhost:5000/callback"

AUTH_URL = "https://accounts.spotify.com/authorize"
TOKEN_URL = "https://accounts.spotify.com/api/token"
API_BASE_URL = "https://api.spotify.com/v1/"


@app.route("/")
def index():
    return "Welcome to Spotify App <a href='/login'>Login with Spotify</a>"

@app.route("/login")
def Login():
    scope = "user-read-private user-read-email"

    params = {
        'client_id': CLIENT_ID,
        'response_type': 'code',
        'scope': scope,
        'redirect_uri': REDIRECT_URI,
        # 'show_dialog': True
    }

    auth_url = f"{AUTH_URL}?{urllib.parse.urlencode(params)}"
    return redirect(auth_url)

@app.route('/callback')
def callback():
    if 'error' in request.args:
        return jsonify({"error": request.args['error']})
    
    if 'code' in request.args:
        req_body = {
            'code': request.args['code'],
            'grant_type': 'authorization_code',
            'redirect_uri': REDIRECT_URI,
            'client_id': CLIENT_ID,
            'client_secret': CLIENT_SECRET
        }

        response = requests.post(TOKEN_URL, data=req_body)
        token_info = response.json()

        session['access_token'] = token_info['access_token']
        session['referesh_token'] = token_info['refresh_token']
        session['expires_at'] = datetime.now().timestamp() + token_info['expires_in']

        return redirect('/playlists')


@app.route('/playlists')
def Get_playlist():
    if 'access_token' not in session:
        return redirect('/login')
    
    if datetime.now().timestamp() > session['expires_at']:
        print("TOKEN EXPIRED. REFRESHING...")
        return redirect('/refresh-token')
    
    headers = {
        'Authorization': f"Bearer {session['access_token']}"
    }

    response = requests.get(API_BASE_URL + 'me/playlists', headers=headers)
    playlists = response.json()

    return jsonify(playlists)
    

@app.route('/refresh-token')
def Refresh_token():
    if 'refresh-token' not in session:
        return redirect('/login')

    if datetime.now().timestamp() > session['expires_at']:
        print("TOKEN EXPIRED. REFRESHING...")
        req_body = {
            'grant_type': 'refresh_token',
            'refresh_token': session['refres_token'],
            'client_id': CLIENT_ID,
            'client_secret': CLIENT_SECRET
        }

        response = requests.post(TOKEN_URL, data=req_body)
        new_token_info = response.json()
        session['access_token'] = new_token_info['access_token']
        session['expires_at'] = datetime.now().timestamp() + new_token_info['expires_in']

        redirect('/playlists')


if __name__ == "__main__":
    app.run(host='0.0.0.0', debug=True)