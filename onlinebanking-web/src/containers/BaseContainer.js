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

    render() {
        return (
            <>
                {
                    this.state.alertMessage && message
                        .info(this.state.alertMessage)
                        .then(() => this.setState({ alertMessage: '' }))
                }
                {
                    this.state.errorMessage && message
                        .error(this.state.errorMessage)
                        .then(() => this.setState({ errorMessage: '' }))
                }
                {
                    this.state.successMessage && message
                        .success(this.state.successMessage)
                        .then(() => this.setState({ successMessage: '' }))
                }
            </>
        );
    }
}

export default BaseContainer;