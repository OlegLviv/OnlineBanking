import React from 'react';
import BaseContainer from '../BaseContainer';
import LoginForm from '../../components/LoginForm/LoginForm';

class HomeContainer extends BaseContainer {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <LoginForm />
                {
                    super.render()
                }
            </div>
        );
    }
}

export default HomeContainer;