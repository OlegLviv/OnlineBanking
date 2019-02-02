import React from 'react';

import {
    Form, Input, Button
} from 'antd';

const onSend = (onSendChangePin, form, event) => {
    event.preventDefault();
    
    form
        .validateFields()
        .then(e => onSendChangePin(e));
}

const changePinForm = ({ onSendChangePin, form }) => (
    <Form onSubmit={e => onSend(onSendChangePin, form, e)}>
        <Form.Item>
            {console.log(form)}
            {form.getFieldDecorator('newPin', {
                rules: [{ required: true, message: 'Please input new pin' }],
            })(
                <Input placeholder="Input new pin" />
            )}
        </Form.Item>
        <Form.Item>
            <Button type="primary" htmlType="submit">
                Change
            </Button>
        </Form.Item>
    </Form>
);

export default Form.create()(changePinForm);