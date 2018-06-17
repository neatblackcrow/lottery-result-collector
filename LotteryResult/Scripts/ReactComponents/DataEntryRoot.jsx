class DataEntryRoot extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Round round="test" date="test" />
            );
    }
}

ReactDOM.render(
    <DataEntryRoot />,
    document.getElementById('DataEntryRoot')
);