import React from 'react';
import { message } from 'antd';

class BaseContainer extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            errorMessage: '',
            alertMessage: '',
            successMessage: ''
        }
    }

    componentDidUpdate(_, prevState) {
        if (this.state.errorMessage && !prevState.errorMessage)
            message
                .error(this.state.errorMessage)
                .then(() => this.setState({ errorMessage: '' }));
        if (this.state.alertMessage && !prevState.alertMessage)
            message.info(this.state.alertMessage)
                .then(() => this.setState({ alertMessage: '' }));
        if (this.state.successMessage && !prevState.successMessage)
            message
                .success(this.state.successMessage)
                .then(() => this.setState({ successMessage: '' }));
    }

    render() {
        return (<></>);
    }
}

export default BaseContainer;