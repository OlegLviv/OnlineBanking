import { publicApi } from '../api/api';

export default {
    sendUser2fa(body) {
        return publicApi
            .post('/api/users/sendUser2fa', body);
    }
}