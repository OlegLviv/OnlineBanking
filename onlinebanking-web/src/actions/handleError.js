export const handleError = (dispatch, error, failFunc) => {
    console.error('error from handle', error);

    if (error && error.response) {
        if(error.response.status >= 400 && error.response.status < 500){
            dispatch(failFunc(error.response));
            return Promise.reject(error.response);
        }
    }
    if(error && error.status && error.status >= 400 && error.status < 500){
        dispatch(failFunc(error));
        return Promise.reject(error);
    }

    dispatch(failFunc(error));
    return Promise.reject(error);
};