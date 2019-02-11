import api from 'axios';

export const ACCESS_TOKE = 'token';

const PROTOCOL = 'https';
const HOST_NAME = 'localhost';
const PORT = 44376;
const PROD_PROTOCOL = 'https';
const PROD_HOST_NAME = '';

const getApiUrl = () => {
    let apiUrl = `${PROTOCOL}://${HOST_NAME}:${PORT}`;

    if (!process || !process.env || process.env.NODE_ENV !== 'development') {
        apiUrl = `${PROD_PROTOCOL}://${PROD_HOST_NAME}`;
    }

    return apiUrl;
}

export const logIn = (token, redirectUrl) => {
    localStorage.setItem(ACCESS_TOKE, token);
    redirectUrl && window.location.replace(redirectUrl);
}

export const logOut = () => {
    localStorage.removeItem(ACCESS_TOKE);
    window.location.replace('/login');
}

export const API_URL = getApiUrl();

export const headerToken = token => {
    if (token === null) {
        throw new Error('Token can not be null');
    }

    return {
        Authorization: `Bearer ${token}`
    };
}

const getPrivateApi = () => {
    if (localStorage.getItem(ACCESS_TOKE) === null) {
        window.location.replace(`/login`);
        return api;
    }
    else {
        return api.create({
            headers: headerToken(localStorage.getItem(ACCESS_TOKE))
        });
    }
};

export const publicApi = api.create(
    {
        baseURL: API_URL
    }
);

export const apiGet = async url => {
    return getPrivateApi()
        .get(`${API_URL}${url}`)
        .then(resp => resp, ({ response }) => {
            if (response.status === 401) {
                window.location.replace('/login');
                return response;
            }
            return Promise.reject(response);
        });
};

export const apiPost = async (url, body) => {
    return getPrivateApi()
        .post(`${API_URL}${url}`, body)
        .then(resp => resp, ({ response }) => {
            if (response.status === 401) {
                window.location.replace('/login');
                return response;
            }
            return Promise.reject(response);
        });
};

export const apiPut = async (url, body) => {
    return getPrivateApi()
        .put(`${API_URL}${url}`, body)
        .then(resp => resp, ({ response }) => {
            if (response.status === 401) {
                window.location.replace('/login');
                return response;
            }
            return Promise.reject(response);
        });
};

export const apiDelete = async url => {
    return getPrivateApi()
        .delete(`${API_URL}${url}`)
        .then(resp => resp, ({ response }) => {
            if (response.status === 401) {
                window.location.replace('/login');
                return response;
            }
            return Promise.reject(response);
        });
};