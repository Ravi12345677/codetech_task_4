fetch('/api/token')
    .then(res => res.json())
    .then(token => {
        const dl = window.WebChat.createDirectLine({ token: token.token });
        window.WebChat.renderWebChat({ directLine: dl }, document.getElementById('webchat'));
    });
