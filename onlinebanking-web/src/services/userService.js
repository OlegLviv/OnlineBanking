import { publicApi, apiGet } from '../api/api';

export default {
    sendUser2fa(body) {
        return publicApi
            .post('/api/users/sendUser2fa', body);
    },
    getCurrentUser() {
        return apiGet('/api/users/getCurrent');
    }
}